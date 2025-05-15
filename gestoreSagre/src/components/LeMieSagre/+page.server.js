import { redirect } from '@sveltejs/kit';

/** @type {import('./$types').PageServerLoad} */
export async function load({ cookies }) {
	const authToken = cookies.get('authToken');
	const userRole = cookies.get('userRole');
	if (!authToken || userRole !== 'Organizzatore')
		throw redirect(302, '/Home');
	return {};
} 