import NotesApi from "../../api/NotesApi";
import NotesGrid from "./NotesGrid";
import "./Styles/MainMenuStyles.css";
import Note from "./Note"
import { useState } from "react";
import { useEffect } from "react";
import { signInRedirect, signOutRedirect } from "../../auth/AuthService";
import ProfileSheet from "./ProfileSheet";
import UserApi from "../../api/UserApi";

export default function MainMenu() {

    const [userState, setUserState] = useState({
        user: null,
        isActive: false
    });

    const [noteState, setNoteState] = useState({
        note: null,
        isActive: false
    });

    const [notesState, setNotesState] = useState({
        notes: []
    })

    const notesApi = new NotesApi();
    const usersApi = new UserApi();

    useEffect(() => {
        notesApi.getNotes().then((result) => {
            setNotesState({
                notes: result
            })
        });
    }, [])

    async function createBlankNote() {
        await notesApi.createNote({
            title: "Введите название",
            text: "Введите текст"
        });
        window.location.reload();
    }

    const updateNote = async (note) => {
        await notesApi.updateNote(note);
    }

    const deleteNote = async (id) => {
        await notesApi.deleteNote(id);
    }

    const openNote = (id) => {
        notesApi.getNote(id).then((result) => {

            setNoteState({
                note: result,
                isActive: true
            });
        });
    }

    const closeNote = () => {
        setNoteState({
            note: noteState.note,
            isActive: false
        });
    }

    const openUserSheet = () => {
        usersApi.getUserInfo().then((result) => {
            setUserState({
                user: result,
                isActive: true
            });
        });
    }

    const closeUserSheet = () => {
        setUserState({
            user: userState.user,
            isActive: false
        });
    }

    const setUserImage = (image) => {
        usersApi.setUserImage(image);
    }

    return (
        <div className="main-menu-container" onClick={() => {closeNote(); closeUserSheet();}}>
            <header className="main-menu-header">
                <h1 className="main-menu-header-label">Notes</h1>
                <div>
                    <button onClick={() => signInRedirect()}>Login</button>
                    <button onClick={() => signOutRedirect()}>Logout</button>
                    <img className="profile-image" onClick={openUserSheet} />
                </div>
            </header>
            <div className="notes-grid-container">
                <NotesGrid notes={notesState.notes} openNote={openNote} />
                <i className="bi bi-plus-circle-fill note-add-icon" onClick={createBlankNote}></i>
            </div>
            {noteState.note != null && noteState.isActive === true &&
                <div className="note-container">
                    <Note note={noteState.note} updateNote={updateNote} deleteNote={deleteNote} />
                </div>
            }
            {userState.user != null && userState.isActive === true &&
                <div className="profile-sheet-container">
                    <ProfileSheet user={userState.user} setUserImage={setUserImage} />
                </div>
            }
        </div>
    );
}