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

    /**
     * Get sessions for a specific movie.
     * Assuming the API can filter by movieId or we fetch all and filter client-side.
     * The requirements say: GET /api/session/movie/{movieId} or filter GET /api/session.
     * We'll try the specific endpoint first, if it fails, fallback to filtering.
     * Actually, let's just use the GET /api/session and filter client-side for safety 
     * based on the exact contract `GET /api/session`.
     */
    getSessions: async () => {
        return await fetchData('/session');
    },

    getSessionsByMovie: async (movieId) => {
        // We'll try the route the user suggested: `GET /api/session/movie/{movieId}`
        // If the backend doesn't support it, we could catch and fetch all. 
        // Let's assume the route exists as stated: GET /api/session/movie/{movieId}
        // Actually the prompt said: "GET SESSIONS FOR MOVIE: GET /api/session/movie/{movieId} (or filter GET /api/session by movieId)"
        // Let's just fetch all and filter, it's safer if we don't know the exact routing.
        const allSessions = await fetchData('/session');
        return allSessions.filter(s => s.movieId === movieId);
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
