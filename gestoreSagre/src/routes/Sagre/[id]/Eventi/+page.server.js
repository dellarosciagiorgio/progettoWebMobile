// +page.server.js per Sagra/[id]/Eventi
import { redirect } from '@sveltejs/kit';

/** @type {import('./$types').PageServerLoad} */
export async function load({ cookies, params }) {
  // Controlla se l'utente è autenticato
  const authToken = cookies.get('authToken');
  const userRole = cookies.get('userRole');
  
  // Se l'utente non è un organizzatore autenticato, reindirizza alla home
  if (!authToken || userRole !== 'Organizzatore') {
    throw redirect(302, '/Home');
  }
  
  // Passa l'ID della sagra al componente
  return {
    sagraId: params.id
  };
}