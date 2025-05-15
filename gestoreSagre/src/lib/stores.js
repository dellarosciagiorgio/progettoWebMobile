import { writable } from 'svelte/store';

export const isAuthenticated = writable(false);
export const userEmail = writable('');
export const role = writable('');

export function initStoresFromCookies() {
    function getCookie(name) {
        if (typeof document === 'undefined') return null;
        const nameEQ = name + "=";
        const cookiePart = document.cookie.split(';');
        for (let i = 0; i < cookiePart.length; i++) {
            let c = cookiePart[i];
            while (c.charAt(0) == ' ')
                c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0)
                return c.substring(nameEQ.length, c.length);
        }
        return null;
    }
    const token = getCookie("authToken");
    const email = getCookie("userEmail");
    const userRole = getCookie("userRole");
    if (token) isAuthenticated.set(true);
    if (email) userEmail.set(email);
    if (userRole) role.set(userRole);
}

export function updateStores(token, email, userRole) {
    isAuthenticated.set(true);
    userEmail.set(email);
    role.set(userRole);
}

export function clearStores() {
    isAuthenticated.set(false);
    userEmail.set('');
    role.set('');
}