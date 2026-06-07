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
            <p>${movie.duration} mins</p>
        `;
        card.addEventListener('click', () => handleMovieSelect(movie));
        container.appendChild(card);
    });
}

async function handleMovieSelect(movie) {
    state.selectedMovieId = movie.id;
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

    // Sort sessions by time
    sessions.sort((a, b) => new Date(a.startTime) - new Date(b.startTime));

    sessions.forEach(session => {
        const div = document.createElement('div');
        div.className = 'session-item';
        
        const dateObj = new Date(session.startTime);
        const dateStr = dateObj.toLocaleDateString();
        const timeStr = dateObj.toLocaleTimeString([], {hour: '2-digit', minute:'2-digit'});

        div.innerHTML = `
            <div>
                <strong>${dateStr}</strong> - ${timeStr}
            </div>
            <button class="btn">Select Seats</button>
        `;
        
        div.querySelector('button').addEventListener('click', () => {
            sessionStorage.setItem('currentSessionId', session.id);
            window.location.href = './pages/tickets/seatSelection.html';
        });

        container.appendChild(div);
    });
}

// --- SEAT SELECTION LOGIC ---
async function initSeatSelection() {
    const sessionId = sessionStorage.getItem('currentSessionId');
    if (!sessionId) {
        showError('No session selected. Please go back to the home page.');
        return;
    }

    try {
        // Since we don't have a direct endpoint to get session details by ID easily (based on contract),
        // we'll just show "Session Details" generically or try to fetch it if there was an endpoint.
        document.getElementById('session-info').textContent = 'Session Details';

        const seats = await ticketsApiService.getSessionSeats(sessionId);
        renderSeats(seats);
    } catch (err) {
        showError('Failed to load seats. Please try again.');
        document.getElementById('seats-loading').classList.add('hidden');
    }

    document.getElementById('btn-checkout').addEventListener('click', () => {
        if (seatState.selectedSeats.length > 0) {
            sessionStorage.setItem('selectedSeats', JSON.stringify(seatState.selectedSeats));
            window.location.href = 'checkout.html';
        }
    });
}

function renderSeats(seats) {
    document.getElementById('seats-loading').classList.add('hidden');
    const container = document.getElementById('seats-container');
    const grid = document.getElementById('seat-grid');
    
    container.classList.remove('hidden');
    document.getElementById('selection-summary').classList.remove('hidden');

    if (!seats || seats.length === 0) {
        grid.innerHTML = '<p>No seats configuration found.</p>';
        return;
    }

    // Sort seats nicely
    seats.sort((a, b) => a.seatNumber.localeCompare(b.seatNumber));

    seats.forEach(seat => {
        const el = document.createElement('div');
        el.className = 'seat';
        el.textContent = seat.seatNumber;

        const status = seat.status.toLowerCase();
        
        if (status === 'available') {
            el.classList.add('available');
            el.addEventListener('click', () => toggleSeatSelection(seat.seatNumber, el));
        } else {
            el.classList.add('sold');
        }

        grid.appendChild(el);
    });
}

function toggleSeatSelection(seatNumber, element) {
    const index = seatState.selectedSeats.indexOf(seatNumber);
    if (index === -1) {
        seatState.selectedSeats.push(seatNumber);
        element.classList.remove('available');
        element.classList.add('selected');
    } else {
        seatState.selectedSeats.splice(index, 1);
        element.classList.remove('selected');
        element.classList.add('available');
    }
    updateCheckoutButton();
}

function updateCheckoutButton() {
    const btn = document.getElementById('btn-checkout');
    const list = document.getElementById('selected-seats-list');
    
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
