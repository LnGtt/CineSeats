/**
 * DOM Manager for CineSeats
 * Handles all UI rendering and DOM manipulation
 */

export function renderMovies(movies, containerId, onSessionSelect) {
    const container = document.getElementById(containerId);
    if (!container) return;
    container.innerHTML = '';

    if (!movies || movies.length === 0) {
        container.innerHTML = '<p>No movies available at the moment.</p>';
        return;
    }

    movies.forEach(movie => {
        const movieCard = document.createElement('div');
        movieCard.className = 'movie-card';
        
        const title = document.createElement('h2');
        title.textContent = movie.title;
        
        const duration = document.createElement('p');
        duration.textContent = `Duration: ${movie.duration}`;

        const sessionsContainer = document.createElement('div');
        sessionsContainer.className = 'session-container';

        movie.sessions.forEach(session => {
            const btn = document.createElement('button');
            btn.className = 'session-btn';
            btn.textContent = `${session.time} - ${session.room}`;
            btn.addEventListener('click', () => {
                onSessionSelect(movie, session);
            });
            sessionsContainer.appendChild(btn);
        });

        movieCard.appendChild(title);
        movieCard.appendChild(duration);
        movieCard.appendChild(sessionsContainer);

        container.appendChild(movieCard);
    });
}

export function renderSeats(seats, containerId, onSeatToggle) {
    const container = document.getElementById(containerId);
    if (!container) return;
    container.innerHTML = '';

    if (!seats || seats.length === 0) {
        container.innerHTML = '<p>No seats available for this session.</p>';
        return;
    }

    seats.forEach(seat => {
        const seatDiv = document.createElement('div');
        seatDiv.className = `seat ${seat.status.toLowerCase()}`;
        seatDiv.textContent = seat.number;
        seatDiv.dataset.number = seat.number;
        seatDiv.dataset.status = seat.status;
        seatDiv.dataset.price = seat.price;

        if (seat.status === 'Available') {
            seatDiv.addEventListener('click', () => {
                const isSelected = seatDiv.classList.contains('selected');
                if (isSelected) {
                    seatDiv.classList.remove('selected');
                } else {
                    seatDiv.classList.add('selected');
                }
                onSeatToggle(seat, !isSelected);
            });
        }

        container.appendChild(seatDiv);
    });
}

export function renderCheckoutSummary(selectedSeats, containerId) {
    const container = document.getElementById(containerId);
    if (!container) return;
    container.innerHTML = '';

    if (!selectedSeats || selectedSeats.length === 0) {
        container.innerHTML = '<p>No seats selected.</p>';
        return;
    }

    let totalPrice = 0;
    const seatNumbers = [];

    selectedSeats.forEach(seat => {
        totalPrice += parseFloat(seat.price);
        seatNumbers.push(seat.number);
    });

    const summaryHTML = `
        <p><strong>Selected Seats:</strong> ${seatNumbers.join(', ')}</p>
        <p><strong>Total Price:</strong> $${totalPrice.toFixed(2)}</p>
    `;
    container.innerHTML = summaryHTML;
}

export function renderSuccess(message, qrCodeData, containerId) {
    const container = document.getElementById(containerId);
    if (!container) return;
    
    container.innerHTML = `
        <div class="success-message">
            <h2>Success!</h2>
            <p>${message}</p>
            <p>Your Ticket QR Code:</p>
            <div class="qr-code">${qrCodeData}</div>
            <br/><br/>
            <button class="btn-primary" onclick="window.location.href='index.html'">Back to Home</button>
        </div>
    `;
}

export function showLoading(containerId) {
    const container = document.getElementById(containerId);
    if (container) {
        container.innerHTML = '<p>Loading...</p>';
    }
}
