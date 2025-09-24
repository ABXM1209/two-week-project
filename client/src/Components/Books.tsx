import {useAtom} from "jotai";
import {AllBooksAtom} from "../atoms/atoms.ts";
import {useCallback, useState} from "react";
import {libraryApi} from "../api-clients.ts";
import {BookForm} from './Forms/BookForm.tsx';
import type {BookDto} from "../generated-ts-client.ts";

export default function Books() {

    const [books, setBooks] = useAtom(AllBooksAtom);
    const [editingBook, setEditingBook] = useState<BookDto | null>(null);
    const [showForm, setShowForm] = useState(false);

    const fetchBooks = useCallback(async () => {
        const allBooks = await libraryApi.getAllBooks();
        setBooks(allBooks);
    }, [setBooks]);

    const handleDelete = async (id: string | undefined) => {
        if (!id) {
            alert("Book has no ID, cannot delete.");
            return;
        }
        await libraryApi.deleteBook(id);
        await fetchBooks();
    };


    return (
        <>
            <h1 style={{ textAlign: "center", fontSize: "20" }}>Books</h1>
            <div style={{ textAlign: "center", fontSize: "10", padding: "20px" }}>
                {books.map((b) => (
                    <div key={b.id} style={{ marginBottom: "0.5rem" }}>
                        {b.title} ({b.pages} pages)
                        <button
                            className="btn btn-secondary btn-sm"
                            onClick={() => {
                                setEditingBook(b);
                                setShowForm(true);
                            }}
                        >
                            Edit
                        </button>
                        <button
                            className="btn btn-accent btn-sm"
                            onClick={() => handleDelete(b.id)}
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
                        setEditingBook(null);
                        setShowForm(true);
                    }}
                >
                    Add new Book
                </button>
            </div>

            {showForm && (
                <BookForm
                    initial={editingBook}
                    onSave={async () => {
                        await fetchBooks();
                        setShowForm(false);
                    }}
                    onCancel={() => setShowForm(false)}
                />
            )}
        </>
    );
}
