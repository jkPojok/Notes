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

    const openNote = (id) => {
        api.getNote(id).then((result) => {
            setNoteState({
                note: result,
                isActive: true
            })
        });
    }

    return (
        <div className="main-menu-container">
            <header className="main-menu-header">
                <h1 className="main-menu-header-label">Notes</h1>
                <span>Nickname</span>
            </header>
            <div className="notes-grid-container">
                <NotesGrid notes={notesState.notes} openNote={openNote} />
            </div>
            {noteState.note !== null && noteState.isActive === true &&
                <Note note={noteState.note} />
            }
        </div>
    );
}