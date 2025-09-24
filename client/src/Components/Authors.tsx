import { useAtom } from "jotai";
import { AllAuthorsAtom } from "../atoms/atoms.ts";
import { useCallback, useState } from "react";
import { libraryApi } from "../api-clients.ts";
import { AuthorForm } from "./Forms/AuthorForm.tsx";
import type { AuthorDto } from "../generated-ts-client.ts";

export default function Authors() {
    const [authors, setAuthors] = useAtom(AllAuthorsAtom);
    const [editingAuthor, setEditingAuthor] = useState<AuthorDto | null>(null);
    const [showForm, setShowForm] = useState(false);

    const fetchAuthors = useCallback(async () => {
        const all = await libraryApi.getAllAuthors();
        setAuthors(all);
    }, [setAuthors]);

    const handleDelete = async (id: string | undefined) => {
        if (!id) {
            alert("Author has no ID, cannot delete.");
            return;
        }
        await libraryApi.deleteAuthor(id);
        await fetchAuthors();
    };

    return (
        <>
            <h1 style={{ textAlign: "center", fontSize: "20" }}>Authors</h1>
            <div style={{ textAlign: "center", fontSize: "10", padding: "20px" }}>
                {authors.map((a) => (
                    <div key={a.id} style={{ marginBottom: "0.5rem" }}>
                        {a.name}
                        <button
                            className="btn btn-secondary btn-sm"
                            onClick={() => {
                                setEditingAuthor(a);
                                setShowForm(true);
                            }}
                        >
                            Edit
                        </button>
                        <button
                            className="btn btn-accent btn-sm"
                            onClick={() => handleDelete(a.id)}
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
                        setEditingAuthor(null);
                        setShowForm(true);
                    }}
                >
                    Add new Author
                </button>
            </div>

            {showForm && (
                <AuthorForm
                    initial={editingAuthor}
                    onSave={async () => {
                        await fetchAuthors();
                        setShowForm(false);
                    }}
                    onCancel={() => setShowForm(false)}
                />
            )}
        </>
    );
}
