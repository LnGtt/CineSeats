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
        return await fetchData('/orders', {
            method: 'POST',
            body: JSON.stringify({
                customerEmail,
                sessionId,
                seatNumbers
            })
        });
    }
};
