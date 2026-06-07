import { getMovies, getSeats, createOrder } from './services/apiService.js';
import { renderMovies, renderSeats, renderCheckoutSummary, renderSuccess, showLoading } from './ui/domManager.js';

// Determine the current page based on DOM elements
document.addEventListener('DOMContentLoaded', () => {
    if (document.getElementById('moviesContainer')) {
        initMovieSelection();
    } else if (document.getElementById('seatsContainer')) {
        initSeatSelection();
    } else if (document.getElementById('checkoutContainer')) {
        initCheckout();
    }
});

// ----------------------------------------------------
// Page: Movie Selection
// ----------------------------------------------------
async function initMovieSelection() {
    // Clear any previous session data when returning to home
    sessionStorage.removeItem('selectedSession');
    sessionStorage.removeItem('selectedSeats');

    showLoading('moviesContainer');
    try {
        const movies = await getMovies();
        renderMovies(movies, 'moviesContainer', (movie, session) => {
            // Save selected session info to sessionStorage and navigate
            const sessionData = {
                movieId: movie.id,
                movieTitle: movie.title,
                sessionId: session.id,
                sessionTime: session.time,
                sessionRoom: session.room
            };
            sessionStorage.setItem('selectedSession', JSON.stringify(sessionData));
            window.location.href = 'pages/seatSelection.html';
        });
    } catch (error) {
        document.getElementById('moviesContainer').innerHTML = '<p>Error loading movies. Please try again later.</p>';
        console.error(error);
    }
}

// ----------------------------------------------------
// Page: Seat Selection
// ----------------------------------------------------
let currentSelectedSeats = [];

async function initSeatSelection() {
    const sessionDataStr = sessionStorage.getItem('selectedSession');
    if (!sessionDataStr) {
        window.location.href = '../index.html'; // Redirect if no session selected
        return;
    }

    const sessionData = JSON.parse(sessionDataStr);
    const sessionInfoEl = document.getElementById('sessionInfo');
    if (sessionInfoEl) {
        sessionInfoEl.textContent = `${sessionData.movieTitle} - ${sessionData.sessionTime} (${sessionData.sessionRoom})`;
    }

    const btnContinue = document.getElementById('btnContinue');
    btnContinue.addEventListener('click', () => {
        if (currentSelectedSeats.length > 0) {
            sessionStorage.setItem('selectedSeats', JSON.stringify(currentSelectedSeats));
            window.location.href = 'checkout.html';
        }
    });

    showLoading('seatsContainer');
    try {
        const seats = await getSeats(sessionData.sessionId);
        renderSeats(seats, 'seatsContainer', (seat, isSelected) => {
            if (isSelected) {
                currentSelectedSeats.push(seat);
            } else {
                currentSelectedSeats = currentSelectedSeats.filter(s => s.number !== seat.number);
            }
            // Enable/disable continue button based on selection
            btnContinue.disabled = currentSelectedSeats.length === 0;
        });
    } catch (error) {
        document.getElementById('seatsContainer').innerHTML = '<p>Error loading seats. Please try again later.</p>';
        console.error(error);
    }
}

// ----------------------------------------------------
// Page: Checkout
// ----------------------------------------------------
function initCheckout() {
    const sessionDataStr = sessionStorage.getItem('selectedSession');
    const selectedSeatsStr = sessionStorage.getItem('selectedSeats');

    if (!sessionDataStr || !selectedSeatsStr) {
        window.location.href = '../index.html'; // Redirect if missing data
        return;
    }

    const sessionData = JSON.parse(sessionDataStr);
    const selectedSeats = JSON.parse(selectedSeatsStr);

    renderCheckoutSummary(selectedSeats, 'summaryContainer');

    const btnPay = document.getElementById('btnPay');
    const emailInput = document.getElementById('customerEmail');

    btnPay.addEventListener('click', async () => {
        const email = emailInput.value.trim();
        if (!email) {
            alert('Please enter a valid email address.');
            return;
        }

        btnPay.disabled = true;
        btnPay.textContent = 'Processing...';

        const seatNumbers = selectedSeats.map(s => s.number);
        const orderData = {
            customerEmail: email,
            sessionId: sessionData.sessionId,
            seatNumbers: seatNumbers
        };

        try {
            const result = await createOrder(orderData);
            if (result.success) {
                // Hide checkout form, show success
                document.getElementById('checkoutContainer').style.display = 'none';
                document.getElementById('linkBack').style.display = 'none';
                renderSuccess(result.message, result.qrCodeData, 'successContainer');
                
                // Clear session storage now that order is complete
                sessionStorage.removeItem('selectedSession');
                sessionStorage.removeItem('selectedSeats');
            } else {
                alert('Payment failed. Please try again.');
                btnPay.disabled = false;
                btnPay.textContent = 'Pay / Confirm';
            }
        } catch (error) {
            alert('An error occurred during checkout.');
            console.error(error);
            btnPay.disabled = false;
            btnPay.textContent = 'Pay / Confirm';
        }
    });
}
