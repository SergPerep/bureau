<script lang="ts">
	import getBlobs from "$lib/fetch/getBlobs";
	import putBlob from "$lib/fetch/putBlob";

	let blobs: string[] = $state([]);
	let file: File | null = null;
	const handleFileChange = (event: Event) => {
		const input = event.target as HTMLInputElement;
		if (input.files?.length) {
			file = input.files[0];
		}
	};
	const handlerSubmit = async (event: Event) => {
		event.preventDefault();
		if (!file) return;
		const formData = new FormData();
		formData.append("file", file);
		const message = await putBlob(file);
		console.log(message);
	};

	$effect(() => {
		getBlobs().then(value => blobs = value);
	});
</script>

<h1>Welcome to SvelteKit</h1>
<p>
	Visit <a href="https://svelte.dev/docs/kit">svelte.dev/docs/kit</a> to read the
	documentation
</p>

<form onsubmit={handlerSubmit} method="post">
	<input type="file" accept="*/*" onchange={handleFileChange} />
	<input type="submit" value="Send" />
</form>

<h1>Blobs</h1>
{#if blobs.length > 0}
	<ul>
		{#each blobs as blob}
			<li><a href="./file?name={blob}">{blob}</a></li>
		{/each}
	</ul>
{:else}
	<p>Cannot load blobs</p>
{/if}
