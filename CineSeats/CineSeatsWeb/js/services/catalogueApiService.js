import { fetchData } from '../utils/api.js';

const BASE_URL = 'http://localhost:5242/api';
export const catalogueApiService = {
    login: async (email, password) => {
        // Aponta para a rota que criamos no C#
        const response = await fetch(`${BASE_URL}/admin/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email: email, password: password })
        });

        if (!response.ok) {
            throw new Error('Falha na comunicação com a API');
        }

        // Grava uma string qualquer apenas para o painel liberar o acesso
        localStorage.setItem('adminToken', 'passe-livre-sem-jwt');

        return await response.json();
    },

    getRooms: async () => {
        return await fetchData('/room');
    },

    createRoom: async (roomNumber, layout) => {
        return await fetchData('/room', {
            method: 'POST',
            body: JSON.stringify({ roomNumber, layout })
        });
    },

    getMovies: async () => {
        return await fetchData('/movie');
    },

    createMovie: async (title, durationMinutes, startDate, endDate) => {
        return await fetchData('/movie', {
            method: 'POST',
            body: JSON.stringify({ title, durationMinutes, startDate, endDate })
        });
    },

    getSessionsByMovie: async (movieId) => {
        return await fetchData(`/session/movie/${movieId}`);
    },

    createSession: async (movieId, roomId, description, startTime, ticketPrice) => {
        return await fetchData('/session', {
            method: 'POST',
            body: JSON.stringify({ movieId, roomId, description, startTime, ticketPrice })
        });
    }
};
