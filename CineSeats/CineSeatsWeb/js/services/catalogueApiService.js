import { fetchData } from '../utils/api.js';

export const catalogueApiService = {
    login: async (email, password) => {
        // Mock Login
        if (email === 'admin@cineseats.com' && password === 'admin') {
            const fakeToken = 'mock-jwt-token-12345';
            localStorage.setItem('adminToken', fakeToken);
            return { success: true, token: fakeToken };
        } else {
            throw new Error('Invalid credentials');
        }
    },

    getRooms: async () => {
        return await fetchData('/room', {}, true);
    },

    createRoom: async (name, capacity) => {
        return await fetchData('/room', {
            method: 'POST',
            body: JSON.stringify({ name, capacity })
        }, true);
    },

    getMovies: async () => {
        return await fetchData('/movie', {}, true);
    },

    createMovie: async (title, description, duration) => {
        return await fetchData('/movie', {
            method: 'POST',
            body: JSON.stringify({ title, description, duration })
        }, true);
    },

    getSessions: async () => {
        return await fetchData('/session', {}, true);
    },

    createSession: async (movieId, roomId, startTime) => {
        return await fetchData('/session', {
            method: 'POST',
            body: JSON.stringify({ movieId, roomId, startTime })
        }, true);
    }
};
