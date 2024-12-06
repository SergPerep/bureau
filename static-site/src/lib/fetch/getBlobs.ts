
import { PUBLIC_API_BASE_URL } from "$env/static/public";

export default async function getBlobs() {
    const res = await fetch(`${PUBLIC_API_BASE_URL}/GetBlobs`);
    try {
        if (!res.ok) {
            return console.error("Failed to pull list of blobs");
        }
    
        const { blobNames } = await res.json();
        return blobNames;
    } catch (error) {
        console.error(error)
    }
};