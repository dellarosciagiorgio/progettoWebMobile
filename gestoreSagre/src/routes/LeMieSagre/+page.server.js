// +page.server.js per LeMieSagre
import { redirect } from '@sveltejs/kit';

/** @type {import('./$types').PageServerLoad} */
export async function load({ cookies }) {
  // Controlla se l'utente è autenticato
  const authToken = cookies.get('authToken');
  const userRole = cookies.get('userRole');
  
  // Se l'utente non è un organizzatore autenticato, reindirizza alla home
  if (!authToken || userRole !== 'Organizzatore') {
    throw redirect(302, '/Home');
  }
  
  // Non carichiamo i dati lato server, lo faremo nel componente client
  return {};
}