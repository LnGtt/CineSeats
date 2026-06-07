const BASE_URL = 'https://localhost:7198/api';

// 1. Domínio: CATALOGUE -> Busca os filmes em cartaz
export async function getMovies() {
    try {
        const response = await fetch(`${BASE_URL}/movie`);
        if (!response.ok) throw new Error('Error fetching movies');
        return await response.json();
    } catch (error) {
        console.error(error);
        return [];
    }
}

// 2. Domínio: CATALOGUE -> Busca as sessões de um filme específico
export async function getSessions(movieId) {
    try {
        const response = await fetch(`${BASE_URL}/session/movie/${movieId}`);
        if (!response.ok) throw new Error('Error fetching sessions');
        return await response.json();
    } catch (error) {
        console.error(error);
        return [];
    }
}

// 3. Domínio: TICKETING -> Busca o mapa de poltronas de uma sessão
export async function getSessionSeats(sessionId) {
    try {
        const response = await fetch(`${BASE_URL}/sessionSeats/${sessionId}`);
        if (!response.ok) throw new Error('Error fetching seats');
        return await response.json();
    } catch (error) {
        console.error(error);
        return [];
    }
}

// 4. Domínio: TICKETING -> Cria o pedido e trava as cadeiras
export async function createOrder(customerEmail, sessionId, seatNumbers) {
    try {
        const payload = {
            customerEmail: customerEmail,
            sessionId: sessionId,
            seatNumbers: seatNumbers
        };

        const response = await fetch(`${BASE_URL}/orders`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(payload)
        });

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(errorData.message || 'Error processing payment');
        }

        return await response.json();
    } catch (error) {
        console.error(error);
        throw error;
    }
}