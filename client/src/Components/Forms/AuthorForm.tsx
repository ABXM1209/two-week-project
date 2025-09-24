import { libraryApi } from "../../api-clients.ts";
import { useState } from "react";
import type { AuthorDto } from "../../generated-ts-client.ts";

type AuthorFormProps = {
    initial: AuthorDto | null;
    onSave: () => void;
    onCancel: () => void;
};

export function AuthorForm({ initial, onSave, onCancel }: AuthorFormProps) {
    const [name, setName] = useState(initial?.name ?? "");
    const isEdit = Boolean(initial?.id);

    const handleSubmit = async () => {
        if (isEdit) {
            if (!initial?.id) {
                alert("Author ID missing, cannot update");
                return;
            }
            await libraryApi.updateAuthor({
                authorId: initial.id,
                name,
                booksIds: initial.booksIds ?? []
            });
        } else {
            await libraryApi.createAuthor({ name });
        }
        onSave();
    };

    return (
        <div style={{ border: "1px solid gray", padding: "1rem", margin: "1rem" }}>
            <h3>{isEdit ? "Edit Author" : "Create Author"}</h3>
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
