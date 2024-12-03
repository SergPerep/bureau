import type { PageLoad } from "./$types";

export const load: PageLoad = ({ url }) => {
    const name = url.searchParams.get('name');
    return { props: { name } }
}