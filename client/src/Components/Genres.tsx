import { useAtom } from "jotai";
import { AllGenresAtom } from "../atoms/atoms.ts";
import { useCallback, useState } from "react";
import { libraryApi } from "../api-clients.ts";
import { GenreForm } from "./Forms/GenreForm.tsx";
import type { GenreDto } from "../generated-ts-client.ts";

export default function Genres() {
    const [genres, setGenres] = useAtom(AllGenresAtom);
    const [editingGenre, setEditingGenre] = useState<GenreDto | null>(null);
    const [showForm, setShowForm] = useState(false);

    const fetchGenres = useCallback(async () => {
        const all = await libraryApi.getAllGenres();
        setGenres(all);
    }, [setGenres]);

    const handleDelete = async (id: string | undefined) => {
        if (!id) {
            alert("Genre has no ID, cannot delete.");
            return;
        }
        await libraryApi.deleteGenre(id);
        await fetchGenres();
    };

    return (
        <>
            <h1 style={{ textAlign: "center", fontSize: "20" }}>Genres</h1>
            <div style={{ textAlign: "center", fontSize: "10", padding: "20px" }}>
                {genres.map((g) => (
                    <div key={g.id} style={{ marginBottom: "0.5rem" }}>
                        {g.name}
                        <button
                            className="btn btn-secondary btn-sm"
                            onClick={() => {
                                setEditingGenre(g);
                                setShowForm(true);
                            }}
                        >
                            Edit
                        </button>
                        <button
                            className="btn btn-accent btn-sm"
                            onClick={() => handleDelete(g.id)}
                        >
                            Delete
                        </button>
                    </div>
                ))}
            </div>

            <div style={{ textAlign: "center", fontSize: "10" }}>
                <button
                    className="btn btn-primary btn-outline"
                    onClick={() => {
                        setEditingGenre(null);
                        setShowForm(true);
                    }}
                >
                    Add new Genre
                </button>
            </div>

            {showForm && (
                <GenreForm
                    initial={editingGenre}
                    onSave={async () => {
                        await fetchGenres();
                        setShowForm(false);
                    }}
                    onCancel={() => setShowForm(false)}
                />
            )}
        </>
    );
}
