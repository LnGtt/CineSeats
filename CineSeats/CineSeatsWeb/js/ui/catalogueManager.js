import { catalogueApiService } from '../services/catalogueApiService.js';
const getPageName = () => {
    const path = window.location.pathname;
    if (path.endsWith('login.html')) return 'login';
    if (path.endsWith('dashboard.html')) return 'dashboard';
    return 'unknown';
};

document.addEventListener('DOMContentLoaded', () => {
    const page = getPageName();

    if (page === 'login') {
        initLogin();
    } else if (page === 'dashboard') {
        initDashboard();
    }
});

// --- LOGIN LOGIC ---
function initLogin() {
    const form = document.getElementById('login-form');
    if (!form) return;

    form.addEventListener('submit', async (e) => {
        e.preventDefault();
        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;
        const errContainer = document.getElementById('login-error');
        
        try {
            errContainer.classList.add('hidden');
            await catalogueApiService.login(email, password);
            window.location.href = 'dashboard.html';
        } catch (err) {
            errContainer.textContent = err.message || 'Login failed';
            errContainer.classList.remove('hidden');
        }
    });
}

// --- DASHBOARD LOGIC ---
let dashboardState = {
    rooms: [],
    movies: [],
    sessions: []
};

function initDashboard() {


    // Sidebar navigation
    document.querySelectorAll('.nav-link[data-target]').forEach(link => {
        link.addEventListener('click', (e) => {
            document.querySelectorAll('.nav-link').forEach(l => l.classList.remove('active'));
            e.target.classList.add('active');
            
            document.querySelectorAll('.tab-content').forEach(tc => tc.classList.remove('active'));
            document.getElementById(e.target.dataset.target).classList.add('active');
        });
    });

    document.getElementById('btn-logout').addEventListener('click', () => {
        window.location.href = 'login.html';
    });

    const movieSelect = document.getElementById('batchMovieId');
    if (movieSelect) {
        movieSelect.addEventListener('change', loadSessions);
    }

    // Load initial data
    loadRooms();
    loadMovies();
    loadSessions();

    // Setup forms
    document.getElementById('form-room').addEventListener('submit', handleRoomSubmit);
    document.getElementById('form-movie').addEventListener('submit', handleMovieSubmit);
    document.getElementById('form-session-batch').addEventListener('submit', handleSessionBatchSubmit);
}

function showMsg(type, msg) {
    const err = document.getElementById('dashboard-error');
    const succ = document.getElementById('dashboard-success');
    err.classList.add('hidden');
    succ.classList.add('hidden');

    if (type === 'error') {
        err.textContent = msg;
        err.classList.remove('hidden');
    } else {
        succ.textContent = msg;
        succ.classList.remove('hidden');
        setTimeout(() => succ.classList.add('hidden'), 3000);
    }
}

// --- ROOMS ---
async function loadRooms() {
    try {
        const rooms = await catalogueApiService.getRooms();
        dashboardState.rooms = rooms;
        renderRoomsTable();
        updateRoomSelects();
    } catch (err) {
        console.error(err);
    }
}

function renderRoomsTable() {
    const tbody = document.querySelector('#table-rooms tbody');
    tbody.innerHTML = '';
    if (!dashboardState.rooms || dashboardState.rooms.length === 0) {
        tbody.innerHTML = '<tr><td colspan="2">No rooms found.</td></tr>';
        return;
    }
    dashboardState.rooms.forEach(r => {
        tbody.innerHTML += `<tr><td>${r.id || '-'}</td><td>${r.roomNumber || r.name || '-'}</td></tr>`;
    });
}

async function handleRoomSubmit(e) {
    e.preventDefault();
    const roomNumber = parseInt(document.getElementById('roomNumber').value, 10);
    const layoutStr = document.getElementById('roomLayout').value;
    try {
        const layout = layoutStr.split(',').map(item => {
            const parts = item.split(':');
            if (parts.length !== 2) throw new Error("Invalid layout format. Use Row:Seats, e.g., A:10, B:15");
            const rowLetter = parts[0].trim();
            const numberOfSeats = parseInt(parts[1].trim(), 10);
            if (!rowLetter || isNaN(numberOfSeats)) throw new Error("Invalid layout format");
            return { rowLetter, numberOfSeats };
        });

        await catalogueApiService.createRoom(roomNumber, layout);
        showMsg('success', 'Room created successfully');
        e.target.reset();
        loadRooms(); // reload
    } catch (err) {
        showMsg('error', err.message);
    }
}

// --- MOVIES ---
async function loadMovies() {
    try {
        const movies = await catalogueApiService.getMovies();
        dashboardState.movies = movies;
        renderMoviesTable();
        updateMovieSelects();
    } catch (err) {
        console.error(err);
    }
}

function renderMoviesTable() {
    const tbody = document.querySelector('#table-movies tbody');
    tbody.innerHTML = '';
    if (!dashboardState.movies || dashboardState.movies.length === 0) {
        tbody.innerHTML = '<tr><td colspan="5">No movies found.</td></tr>';
        return;
    }
    dashboardState.movies.forEach(m => {
        tbody.innerHTML += `<tr><td>${m.id || '-'}</td><td>${m.title}</td><td>${m.durationMinutes || m.duration || '-'}</td><td>${m.startDate || '-'}</td><td>${m.endDate || '-'}</td></tr>`;
    });
}

async function handleMovieSubmit(e) {
    e.preventDefault();
    const title = document.getElementById('movieTitle').value;
    const durationMinutes = parseInt(document.getElementById('movieDuration').value, 10);
    const startDate = document.getElementById('movieStartDate').value;
    const endDate = document.getElementById('movieEndDate').value;
    try {
        await catalogueApiService.createMovie(title, durationMinutes, startDate, endDate);
        showMsg('success', 'Movie created successfully');
        e.target.reset();
        loadMovies();
    } catch (err) {
        showMsg('error', err.message);
    }
}

// --- SESSIONS ---
async function loadSessions() {
    // Pega o ID do filme selecionado na lista suspensa do formulário
    const movieId = document.getElementById('batchMovieId').value;
    
    if (!movieId) {
        dashboardState.sessions = [];
        renderSessionsTable();
        return;
    }

    try {
        // Busca apenas as sessões do filme escolhido
        dashboardState.sessions = await catalogueApiService.getSessionsByMovie(movieId);
        renderSessionsTable();
    } catch (err) {
        console.error('Failed to load sessions', err);
        dashboardState.sessions = [];
        renderSessionsTable();
    }
}

function renderSessionsTable() {
    const tbody = document.querySelector('#table-sessions tbody');
    tbody.innerHTML = '';
    if (!dashboardState.sessions || dashboardState.sessions.length === 0) {
        tbody.innerHTML = '<tr><td colspan="5">No sessions found.</td></tr>';
        return;
    }
    dashboardState.sessions.forEach(s => {
        tbody.innerHTML += `<tr>
            <td>${s.id || '-'}</td>
            <td>${s.movieId}</td>
            <td>${s.roomId}</td>
            <td>${s.startTime || '-'}</td>
            <td>$${(s.ticketPrice || 0).toFixed(2)}</td>
        </tr>`;
    });
}

function updateRoomSelects() {
    const select = document.getElementById('batchRoomId');
    if (!select) return;
    
    select.innerHTML = '';
    const defaultOption = document.createElement('option');
    defaultOption.value = '';
    defaultOption.textContent = 'Select Room...';
    select.appendChild(defaultOption);

    dashboardState.rooms.forEach(room => {
        const option = document.createElement('option');
        option.value = room.id || room.Id;
        option.textContent = room.roomNumber || room.RoomNumber || room.name || room.Name;
        select.appendChild(option);
    });
}

function updateMovieSelects() {
    const select = document.getElementById('batchMovieId');
    if (!select) return;
    
    select.innerHTML = '';
    const defaultOption = document.createElement('option');
    defaultOption.value = '';
    defaultOption.textContent = 'Select Movie...';
    select.appendChild(defaultOption);

    dashboardState.movies.forEach(movie => {
        const option = document.createElement('option');
        option.value = movie.id || movie.Id;
        option.textContent = movie.title || movie.Title;
        select.appendChild(option);
    });
}

async function handleSessionBatchSubmit(e) {
    e.preventDefault();
    const movieId = document.getElementById('batchMovieId').value;
    const roomId = document.getElementById('batchRoomId').value;
    const description = document.getElementById('sessionDescription').value;
    let startTime = document.getElementById('sessionStartTime').value;
    const ticketPrice = parseFloat(document.getElementById('sessionTicketPrice').value);

    const btn = document.getElementById('btn-create-batch');
    btn.disabled = true;
    btn.textContent = 'Generating...';

    try {
        if (startTime && startTime.split(':').length === 2) {
            startTime += ':00'; // Make it "14:30:00" for TimeOnly if required
        }

        await catalogueApiService.createSession(movieId, roomId, description, startTime, ticketPrice);
        
        showMsg('success', `Session created successfully.`);
        e.target.reset();
        loadSessions();

    } catch (err) {
        showMsg('error', err.message);
    } finally {
        btn.disabled = false;
        btn.textContent = 'Create Session';
    }
}
