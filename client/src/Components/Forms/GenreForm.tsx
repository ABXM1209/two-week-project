import { libraryApi } from "../../api-clients.ts";
import { useState } from "react";
import type { GenreDto } from "../../generated-ts-client.ts";

type GenreFormProps = {
    initial: GenreDto | null;
    onSave: () => void;
    onCancel: () => void;
};

export function GenreForm({ initial, onSave, onCancel }: GenreFormProps) {
    const [name, setName] = useState(initial?.name ?? "");
    const isEdit = Boolean(initial?.id);

    const handleSubmit = async () => {
        if (isEdit) {
            if (!initial?.id) {
                alert("Genre ID missing, cannot update");
                return;
            }
            await libraryApi.updateGenre({
                genreId: initial.id,
                name,
                booksIds: initial.booksIds ?? [] // 👈 required by backend
            });
        } else {
            await libraryApi.createGenre({ name });
        }
        onSave();
    };

    return (
        <div style={{ border: "1px solid gray", padding: "1rem", margin: "1rem" }}>
            <h3>{isEdit ? "Edit Genre" : "Create Genre"}</h3>
            <input
                placeholder="Name"
                value={name}
                onChange={(e) => setName(e.target.value)}
            />

            <div style={{ marginTop: "0.5rem" }}>
                <button onClick={handleSubmit} className="btn btn-primary">
                    Save
                </button>
                <button onClick={onCancel} className="btn btn-secondary">
                    Cancel
                </button>
            </div>
        </div>
    );
}
