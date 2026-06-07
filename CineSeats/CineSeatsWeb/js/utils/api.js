export const BASE_URL = 'https://localhost:7198/api';

/**
 * Core fetch wrapper.
 * @param {string} endpoint - The API endpoint starting with '/'
 * @param {object} options - Fetch options (method, body, headers, etc)
 * @param {boolean} requiresAuth - Whether to inject Admin JWT
 */
export async function fetchData(endpoint, options = {}, requiresAuth = false) {
    const defaultHeaders = {
        'Content-Type': 'application/json',
        'Accept': 'application/json'
    };

    if (requiresAuth) {
        const token = localStorage.getItem('adminToken');
        if (token) {
            defaultHeaders['Authorization'] = `Bearer ${token}`;
        } else {
            // If strictly required and no token, we could redirect to login immediately
            console.warn('Authentication token missing for protected route');
        }
    }

    const config = {
        ...options,
        headers: {
            ...defaultHeaders,
            ...options.headers
        }
    };

    // Remove Content-Type if we're not sending a body (e.g., GET requests)
    if (!config.body) {
        delete config.headers['Content-Type'];
    }

    try {
        const response = await fetch(`${BASE_URL}${endpoint}`, config);
        
        if (response.status === 401 || response.status === 403) {
            // Handle unauthorized (redirect to login if needed)
            if (requiresAuth && window.location.pathname.includes('/catalogue/')) {
                window.location.href = '/CineSeatsWeb/pages/catalogue/login.html';
            }
            throw new Error('Unauthorized');
        }

        if (!response.ok) {
            const errorData = await response.text();
            throw new Error(errorData || `HTTP error! status: ${response.status}`);
        }

        // Some endpoints might return empty body (e.g. DELETE or some POSTs)
        const text = await response.text();
        return text ? JSON.parse(text) : null;

    } catch (error) {
        console.error(`API Error on ${endpoint}:`, error);
        throw error;
    }
}
