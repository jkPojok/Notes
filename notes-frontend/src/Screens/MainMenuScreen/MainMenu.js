import NotesApi from "../../api/NotesApi";
import NotesGrid from "./NotesGrid";
import "./Styles/MainMenuStyles.css";
import Note from "./Note"
import { useState } from "react";
import { useEffect } from "react";
import { isLoggedIn, signInRedirect, signOutRedirect } from "../../auth/AuthService";
import BackgroundImagesMenu from "./BackgroundImagesMenu";
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

    const [userImagesState, setUserImagesState] = useState({
        image: null
    });

    const [noteState, setNoteState] = useState({
        note: null,
        isActive: false
    });

    const [notesState, setNotesState] = useState({
        notes: []
    })

    const [backgroundMenuState, setBackgroundMenuState] = useState({
        isActive: false
    });

    const notesApi = new NotesApi();
    const usersApi = new UserApi();

    useEffect(() => {
        isLoggedIn().then((result) => {
            setUserLoggedInState({
                isLoggedIn: result
            })
        });

        usersApi.getPrimaryUserInfo().then((result) => {
            setUserImagesState({
                image: result ? result.userImageLink : null
            })
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

    const openBackgroundImagesMenu = () => {
        setBackgroundMenuState({isActive: true});
    }

    const closeBackgroundImagesMenu = () => {
        if (backgroundMenuState.isActive === true) {
            setBackgroundMenuState({isActive: false});
        }
    }

    const setUserImage = (image) => {
        usersApi.setUserImage(image);
    }

    const updateAboutMe = (aboutMeText) => {
        usersApi.updateAboutMe(aboutMeText);
    }

    const setUserGender = (gender) => {
        usersApi.setUserGender(gender);
    }

    const deleteUser = async () => {
        await usersApi.deleteUser();
        signOutRedirect();
    }

    return (
        <div className="main-menu-container" style={{background: `url(${localStorage.getItem('bgImg')})`}} onClick={() => {closeNote(); closeUserSheet(); closeBackgroundImagesMenu();}}>
            <header className="main-menu-header">
                <h1 className="main-menu-header-label">My NotesX</h1>
                <div>
                    {isUserLoggedInState.isLoggedIn == false && <button onClick={() => signInRedirect()}>Войти</button>}
                    {isUserLoggedInState.isLoggedIn == true &&
                    <div>
                        <button onClick={() => signOutRedirect()}>Выход</button>
                        <img className="profile-image mb-1" src={userImagesState.image} onClick={openUserSheet} />
                    </div>}
                </div>
            </header>

            <div className="notes-grid-container">
                <NotesGrid notes={notesState.notes} openNote={openNote} createBlankNote={createBlankNote} />
            </div>
            {noteState.note != null && noteState.isActive === true &&
                <div className="note-container">
                    <Note note={noteState.note} updateNote={updateNote} deleteNote={deleteNote} />
                </div>
            }
            {userState.user != null && userState.isActive === true &&
                <div className="profile-sheet-container">
                    <ProfileSheet user={userState.user} setUserImage={setUserImage} updateAboutMe={updateAboutMe}
                    deleteUser={deleteUser} setUserGender={setUserGender} />
                </div>
            }
            {backgroundMenuState.isActive === true &&
                <div className="background-menu-container">
                    <BackgroundImagesMenu />
                </div>
            }
            
            <footer className="main-menu-footer">
                <div className="main-menu-footer-container">
                    <span>Время: </span>
                    <span>Пользовательское соглашение</span>
                    <span>©2023 My NotesX</span>
                </div>
                <div>
                    <button className="pallete-button bi bi-palette-fill" onClick={openBackgroundImagesMenu} /> 
                </div>
            </footer>
        </div>
    );
}