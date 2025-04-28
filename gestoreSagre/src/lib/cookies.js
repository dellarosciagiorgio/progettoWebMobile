export function getCookie(name) {
    const cookies = document.cookie.split(';');
    for (let i = 0; i < cookies.length; i++) {
        let cookie = cookies[i].trim();
        if (cookie.startsWith(name + '='))
            return cookie.substring(name.length + 1, cookie.length);
    }
    return null; // todo: replace not found warning
}