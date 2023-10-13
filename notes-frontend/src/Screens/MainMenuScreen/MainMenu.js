import NotesApi from "../../api/NotesApi";
import NotesGrid from "./NotesGrid";
import "./Styles/MainMenuStyles.css";
import Note from "./Note"
import { useState } from "react";
import { useEffect } from "react";
import { isLoggedIn, signInRedirect, signOutRedirect } from "../../auth/AuthService";
import ProfileSheet from "./ProfileSheet";
import UserApi from "../../api/UserApi";

export default function MainMenu() {

    const [isUserLoggedInState, setUserLoggedInState] = useState({
        isLoggedIn: false
    });

    const [userState, setUserState] = useState({
        user: null,
        isActive: false
    });

    const [userImageState, setUserImageState] = useState({
        image: null
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
        isLoggedIn().then((result) => {
            setUserLoggedInState({
                isLoggedIn: result
            })
        });

        usersApi.getUserImage().then((result) => {
            setUserImageState({image: result})
        });

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

    const updateAboutMe = (aboutMeText) => {
        usersApi.updateAboutMe(aboutMeText);
    }

    const deleteUser = async () => {
        await usersApi.deleteUser();
        signOutRedirect();
    }

    return (
        <div className="main-menu-container" onClick={() => {closeNote(); closeUserSheet();}}>
            <header className="main-menu-header">
                <h1 className="main-menu-header-label">Notes</h1>
                <div>
                    {isUserLoggedInState.isLoggedIn == false && <button onClick={() => signInRedirect()}>Login</button>}
                    {isUserLoggedInState.isLoggedIn == true &&
                    <div>
                        <button onClick={() => signOutRedirect()}>Logout</button>
                        <img className="profile-image" src={userImageState.image} onClick={openUserSheet} />
                    </div>}
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
                    <ProfileSheet user={userState.user} setUserImage={setUserImage} updateAboutMe={updateAboutMe} deleteUser={deleteUser} />
                </div>
            }
        </div>
    );
}