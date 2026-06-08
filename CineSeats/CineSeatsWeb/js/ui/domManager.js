import { ticketsApiService } from '../services/ticketsApiService.js';
// Utility to parse URL path to determine current page
const getPageName = () => {
    const path = window.location.pathname;
    if (path.endsWith('seatSelection.html')) return 'seatSelection';
    if (path.endsWith('checkout.html')) return 'checkout';
    if (path.endsWith('index.html') || path.endsWith('/')) return 'index';
    return 'unknown';
};

// Global state for index page
let state = {
    selectedMovieId: null,
    movies: []
};

// Global state for seat selection
let seatState = {
    selectedSeats: []
};

document.addEventListener('DOMContentLoaded', () => {
    const page = getPageName();

    if (page === 'index') {
        initIndex();
    } else if (page === 'seatSelection') {
        initSeatSelection();
    } else if (page === 'checkout') {
        initCheckout();
    }
});

function showError(msg) {
    const errContainer = document.getElementById('error-container');
    if (errContainer) {
        errContainer.textContent = msg;
        errContainer.classList.remove('hidden');
    } else {
        alert(msg);
    }
}

// --- INDEX PAGE LOGIC ---
async function initIndex() {
    try {
        const movies = await ticketsApiService.getMovies();
        state.movies = movies;
        renderMovies(movies);
    } catch (err) {
        showError('Failed to load movies. Please try again later.');
        document.getElementById('movies-loading').classList.add('hidden');
    }
    
    const backBtn = document.getElementById('btn-back-movies');
    if (backBtn) {
        backBtn.addEventListener('click', () => {
            document.getElementById('sessions-section').classList.add('hidden');
            document.getElementById('movies-container').classList.remove('hidden');
            state.selectedMovieId = null;
        });
    }
}

function renderMovies(movies) {
    const loading = document.getElementById('movies-loading');
    const container = document.getElementById('movies-container');
    
    if (loading) loading.classList.add('hidden');
    if (!container) return;

    container.innerHTML = '';
    container.classList.remove('hidden');

    if (!movies || movies.length === 0) {
        container.innerHTML = '<p>No movies currently playing.</p>';
        return;
    }

    movies.forEach(movie => {
        const card = document.createElement('div');
        card.className = 'card movie-card';
        card.innerHTML = `
            <h3>${movie.title}</h3>
            <p>${movie.durationMinutes} mins</p>
        `;
        card.addEventListener('click', () => handleMovieSelect(movie));
        container.appendChild(card);
    });
}

async function handleMovieSelect(movie) {
    state.selectedMovieId = movie.id;

    // CORREÇÃO 3A: Guardar os detalhes do filme na memória para a tela das cadeiras conseguir ler
    sessionStorage.setItem('selectedMovie', JSON.stringify(movie));

    document.getElementById('movies-container').classList.add('hidden');

    const sessionsSection = document.getElementById('sessions-section');
    sessionsSection.classList.remove('hidden');

    document.getElementById('selected-movie-title').textContent = `Sessions for ${movie.title}`;

    const sessionsContainer = document.getElementById('sessions-container');
    sessionsContainer.innerHTML = '<p>Loading sessions...</p>';

    try {
        const sessions = await ticketsApiService.getSessionsByMovie(movie.id);
        renderSessions(sessions);
    } catch (err) {
        sessionsContainer.innerHTML = '<p style="color:red">Failed to load sessions.</p>';
    }
}

function renderSessions(sessions) {
    const container = document.getElementById('sessions-container');
    container.innerHTML = '';

    if (!sessions || sessions.length === 0) {
        container.innerHTML = '<p>No sessions available for this movie.</p>';
        return;
    }

    // Remover ordenação por Date() já que a API só envia a Hora
    sessions.forEach(session => {
        const div = document.createElement('div');
        div.className = 'session-item';

        // CORREÇÃO 2: Apenas imprimir a hora pura da API, sem usar new Date()
        div.innerHTML = `
            <div>
                <strong>Horário:</strong> ${session.startTime}
            </div>
            <button class="btn">Select Seats</button>
        `;

        div.querySelector('button').addEventListener('click', () => {
            // CORREÇÃO 3B: O nome da variável tem de ser exatamente 'selectedSessionId'
            sessionStorage.setItem('selectedSessionId', session.id);

            // CORREÇÃO 3C: Caminho limpo para evitar o 404 no Rider
            window.location.href = 'pages/tickets/seatSelection.html';
        });

        container.appendChild(div);
    });
}

// --- SEAT SELECTION LOGIC ---
async function initSeatSelection() {
    const sessionId = sessionStorage.getItem('selectedSessionId');
    const movieJson = sessionStorage.getItem('selectedMovie');

    if (!sessionId || !movieJson) {
        window.location.href = 'index.html';
        return;
    }

    const movie = JSON.parse(movieJson);

    document.getElementById('session-info').textContent = `${movie.title} - ${movie.durationMinutes} minutos`;

    try {
        const seats = await ticketsApiService.getSessionSeats(sessionId);
        renderSeatsGrid(seats);
    } catch (err) {
        const loading = document.getElementById('seats-loading');
        if (loading) {
            loading.textContent = 'Erro ao carregar mapa de cadeiras. Verifique se a API está ligada.';
            loading.style.color = 'red';
        }
    }

    const btnCheckout = document.getElementById('btn-checkout');
    if (btnCheckout) {
        btnCheckout.addEventListener('click', () => {
            sessionStorage.setItem('currentSessionId', sessionId);
            sessionStorage.setItem('selectedSeats', JSON.stringify(seatState.selectedSeats));
            window.location.href = 'checkout.html';
        });
    }
}

function renderSeatsGrid(seats) {
    const container = document.getElementById('seats-container');
    const loading = document.getElementById('seats-loading');
    if (!container || !loading) return;

    loading.classList.add('hidden');
    container.classList.remove('hidden');
    
    const summary = document.getElementById('selection-summary');
    if (summary) summary.classList.remove('hidden');

    const grid = document.getElementById('seat-grid');
    if (!grid) return;
    grid.innerHTML = '';

    if (!seats || seats.length === 0) {
        grid.innerHTML = '<p>Nenhuma cadeira configurada para esta sessão.</p>';
        return;
    }

    seats.forEach(seat => {
        const div = document.createElement('div');
        
        // Mapeia o status em português da API para as classes CSS
        let cssClass = 'available';
        if (seat.status === 'Vendido') cssClass = 'sold';
        if (seat.status === 'Reservado') cssClass = 'reserved';

        div.className = `seat ${cssClass}`;
        div.textContent = seat.seatNumber;

        if (cssClass === 'available') {
            div.addEventListener('click', () => {
                const index = seatState.selectedSeats.indexOf(seat.seatNumber);
                if (index === -1) {
                    seatState.selectedSeats.push(seat.seatNumber);
                    div.classList.remove('available');
                    div.classList.add('selected');
                } else {
                    seatState.selectedSeats.splice(index, 1);
                    div.classList.remove('selected');
                    div.classList.add('available');
                }
                updateCheckoutButton();
            });
        }
        grid.appendChild(div);
    });
}

function updateCheckoutButton() {
    const btn = document.getElementById('btn-checkout');
    const list = document.getElementById('selected-seats-list');
    
    if (!btn || !list) return;

    if (seatState.selectedSeats.length > 0) {
        list.textContent = seatState.selectedSeats.join(', ');
        btn.classList.remove('btn-disabled');
        btn.disabled = false;
    } else {
        list.textContent = 'None';
        btn.classList.add('btn-disabled');
        btn.disabled = true;
    }
}

// --- CHECKOUT LOGIC ---
function initCheckout() {
    const sessionId = sessionStorage.getItem('currentSessionId');
    const selectedSeatsJson = sessionStorage.getItem('selectedSeats');

    if (!sessionId || !selectedSeatsJson) {
        showError('Missing order information. Please start over.');
        return;
    }

    const selectedSeats = JSON.parse(selectedSeatsJson);
    document.getElementById('summary-seats').textContent = selectedSeats.join(', ');

    const form = document.getElementById('checkout-form');
    form.addEventListener('submit', async (e) => {
        e.preventDefault();
        const email = document.getElementById('customerEmail').value;
        const btn = document.getElementById('btn-submit-order');
        
        try {
            btn.textContent = 'Processing...';
            btn.disabled = true;
            btn.classList.add('btn-disabled');

            const orderResponse = await ticketsApiService.createOrder(email, sessionId, selectedSeats);
            
            // Show Success
            document.getElementById('checkout-form-container').classList.add('hidden');
            document.getElementById('success-container').classList.remove('hidden');
            
            // Generate some fake QR string representation
            const qrString = btoa(`${email}-${sessionId}-${selectedSeats.join(',')}`);
            document.getElementById('qr-string').textContent = `Order-Ref: ${qrString}`;

            // Clean up
            sessionStorage.removeItem('selectedSeats');

        } catch (err) {
            showError('Failed to complete order. ' + err.message);
            btn.textContent = 'Complete Order';
            btn.disabled = false;
            btn.classList.remove('btn-disabled');
        }
    });
}
