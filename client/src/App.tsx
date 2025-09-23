import {createBrowserRouter, RouterProvider} from "react-router";
import Home from "./Components/Home.tsx";
import {useEffect} from "react";
import {libraryApi} from "./api-clients.ts";
import {useAtom} from "jotai";
import {AllAuthorsAtom, AllBooksAtom, AllGenresAtom} from "./atoms/atoms.ts";
import Books from "./Components/Books.tsx";
import Authors from "./Components/Authors.tsx";
import Genres from "./Components/Genres.tsx";


function App() {

    const [, setAuthors] = useAtom(AllAuthorsAtom)
    const [, setBooks] = useAtom(AllBooksAtom)
    const [, setGenres] = useAtom(AllGenresAtom)

    useEffect(() => {
        initializeData();
    }, [])

    async function initializeData() {
        setAuthors(await libraryApi.getAllAuthors());
        setBooks(await libraryApi.getAllBooks())
        setGenres(await libraryApi.getAllGenres())
    }

    return (
        <>
            <RouterProvider router={createBrowserRouter([
                {
                    path: '',
                    element: <Home/>,
                    children: [
                        {
                            path: 'books',
                            element: <Books/>
                        },
                        {
                            path: 'authors',
                            element: <Authors/>
                        },
                        {
                            path: 'genres',
                            element: <Genres/>
                        }
                    ]
                }
            ])}/>
        </>
    )
}

export default App
