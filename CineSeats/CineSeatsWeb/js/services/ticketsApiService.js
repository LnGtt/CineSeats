import { fetchData } from '../utils/api.js';
/**
 * Service for customer ticket operations
 */
export const ticketsApiService = {
    /**
     * Get all movies
     */
    getMovies: async () => {
        return await fetchData('/movie');
    },
    
    getSessions: async () => {
        return await fetchData('/session');
    },

    getSessionsByMovie: async (movieId) => {
        return await fetchData(`/session/movie/${movieId}`);
    },

    /**
     * Get seats for a session
     */
    getSessionSeats: async (sessionId) => {
        return await fetchData(`/sessionSeats/${sessionId}`);
    },

    /**
     * Create an order
     */
    createOrder: async (customerEmail, sessionId, seatNumbers) => {
        return await fetchData('/Ticket/order', {
            method: 'POST',
            body: JSON.stringify({
                customerEmail,
                sessionId,
                seatNumbers
            })
        });
    },

    /**
     * Get session by id
     */
    getSessionById: async (sessionId) => {
        return await fetchData(`/session/${sessionId}`);
    },

    /**
     * Process mock payment
     */
    processMockPayment: async (orderId) => {
        return await fetchData(`/Payments/mock-checkout/${orderId}`, {
            method: 'POST'
        });
    }
};
