import NotesApi from "../../api/NotesApi";
import NotesGrid from "./NotesGrid";
import "./Styles/MainMenuStyles.css";
import Note from "./Note"
import { useState } from "react";
import { useEffect } from "react";

export default function MainMenu() {

    const [noteState, setNoteState] = useState({
        note: null,
        isActive: false
    });

    const [notesState, setNotesState] = useState({
        notes: []
    })

    const api = new NotesApi();

    useEffect(() => {
        console.log(api.getNotes());
        api.getNotes().then((result) => {
            setNotesState({
                notes: result
            })
        });
    }, [])

    async function createBlankNote() {
        await api.createNote({
            title: "Введите название",
            text: "Введите текст"
        });
        window.location.reload();
    }
    
    const updateNote = (note) => {
        api.updateNote(note);
    }

    const deleteNote = (id) => {
        api.deleteNote(id);
    }

    const openNote = (id) => {
        api.getNote(id).then((result) => {
            setNoteState({
                note: result,
                isActive: true
            })
        });
    }

    const closeNote = () => {
        setNoteState({
            note: noteState.note,
            isActive: false
        });
    }

    return (
        <div className="main-menu-container" onClick={closeNote}>
            <header className="main-menu-header">
                <h1 className="main-menu-header-label">Notes</h1>
                <div>
                    <button>Login</button>
                    <button>Register</button>
                </div>
            </header>
            <div className="notes-grid-container">
                <NotesGrid notes={notesState.notes} openNote={openNote} />
                <i className="bi bi-plus-circle-fill note-add-icon" onClick={createBlankNote}></i>
            </div>
            {noteState.note !== null && noteState.isActive === true &&
            <div className="note-container">
                <Note note={noteState.note} updateNote={updateNote} deleteNote={deleteNote} />
            </div>
            }
        </div>
    );
}