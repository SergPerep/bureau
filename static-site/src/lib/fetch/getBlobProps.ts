
import { PUBLIC_API_BASE_URL } from '$env/static/public';

export default async function getBlobProps(blobName: string) {
    try {
        const res = await fetch(`${PUBLIC_API_BASE_URL}/GetBlobProps?blobName=${blobName}`);
        const result = await res.json();
        console.log({result});
        return result;
    } catch (error) {
        console.error(error)
    }
}