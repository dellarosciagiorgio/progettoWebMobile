import { redirect } from '@sveltejs/kit';

export function load(redirectPage) {
    throw redirect(302, redirectPage);
}