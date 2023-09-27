import NotesApi from "../../api/NotesApi";
import "./Styles/MainMenuStyles.css";

export default function MainMenu() {

    const api = new NotesApi();
    
    const getNotes = () => {
        console.log(api.getNotes());
    }

    const getNote = () => {
        console.log(api.getNote('a22d9bd3-408c-4ac4-913c-1a8d6e6bfb74'));
    }

    const createNote = () => {
        console.log(api.createNote({ Title: 'Day 1', Text: 'Today is first day' }));
    }

    const updateNote = () => {
        console.log(api.updateNote({ Id: 'a22d9bd3-408c-4ac4-913c-1a8d6e6bfb74', Title: 'Day 2', Text: 'Today is second day' }));
    }

    const deleteNote = () => {
        console.log(api.deleteNote('a22d9bd3-408c-4ac4-913c-1a8d6e6bfb74'));
    }

    return (
        <div className="main-menu-container">
            <header className="main-menu-header">
                <h1 className="main-menu-header-label">Notes</h1>
                <span>Nickname</span>
            </header>
            <div>
                <button onClick={() => getNotes()}>Get Notes</button>
                <button onClick={() => getNote()}>Get Note</button>
                <button onClick={() => createNote()}>Create Note</button>
                <button onClick={() => updateNote()}>Update Note</button>
                <button onClick={() => deleteNote()}>Delete Note</button>
            </div>
        </div>
    );
}