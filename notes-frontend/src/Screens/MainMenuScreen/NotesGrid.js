import "../MainMenuScreen/Styles/NotesGridStyles.css";
import PreviewNote from "./PreviewNote";
import NoteToCreate from "./NoteToCreate";

export default function NotesGrid(props) {

    const openNote = (id) => {
        props.openNote(id);
    }

    return (
        <div className="notes-grid-black-container">
            <div className="notes-grid">
                {Array.isArray(props.notes) && props.notes.length > 0 &&
                    props.notes.map(note => (
                        <PreviewNote note={note} key={note.id} onOpenNote={openNote} />
                    ))}
                <NoteToCreate createBlankNote={props.createBlankNote}/>
            </div>
        </div>
    );
}