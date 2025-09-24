import { libraryApi } from "../../api-clients.ts";
import { useState } from "react";
import type {BookDto} from "../../generated-ts-client.ts";

type BookFormProps = {
    initial: BookDto | null;
    onSave: () => void;
    onCancel: () => void;
};

export function BookForm({ initial, onSave, onCancel }: BookFormProps) {
    const [title, setTitle] = useState(initial?.title ?? "");
    const [pages, setPages] = useState(initial?.pages ?? 0);
    const [genreId, setGenreId] = useState(initial?.genre?.id ?? "");
    const [authorsIds, setAuthorsIds] = useState<string[]>(
        initial?.authorsIds ?? []
    );

    const isEdit = Boolean(initial?.id);

    const handleSubmit = async () => {
        if (isEdit) {
            if (!initial?.id) {
                alert("Book ID missing, cannot update");
                return;
            }

            await libraryApi.updateBook({
                bookId: initial.id,
                title,
                pages,
                genreId: genreId || "00000000-0000-0000-0000-000000000000",
                //authorsIds: authorsIds.length > 0 ? authorsIds : []
                authorsIds: authorsIds ?? []
            });
        } else {
            await libraryApi.createBook({
                title,
                pages,
            });
        }
        onSave();
    };


    return (
        <div style={{ border: "1px solid gray", padding: "1rem", margin: "1rem" }}>
            <h3>{isEdit ? "Edit Book" : "Create Book"}</h3>

            <input
                placeholder="Title"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
            />
            <input
                type="number"
                placeholder="Pages"
                value={pages}
                onChange={(e) => setPages(Number(e.target.value))}
            />
            <input
                placeholder="Genre Id"
                value={genreId}
                onChange={(e) => setGenreId(e.target.value)}
            />
            <input
                placeholder="Authors Ids (comma-separated)"
                value={authorsIds.join(",")}
                onChange={(e) =>
                    setAuthorsIds(
                        e.target.value.split(",").map((s) => s.trim()).filter(Boolean)
                    )
                }
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