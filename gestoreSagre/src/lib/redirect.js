import { redirect } from '@sveltejs/kit';

export function loadTerms(redirectPage) {
    throw redirect(302, redirectPage);
}
export function loadHome(redirectPage) {
    throw redirect(302, redirectPage);
}