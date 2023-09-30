import "../MainMenuScreen/Styles/NotePreviewStyles.css";

export default function PreviewNote(props) {

    const openNote = () => {
        props.onOpenNote(props.note.id);
    }

    return (
        <div className="note-preview" onClick={() => openNote()}>
            <span className="note-preview-title">{props.note.title}</span>
            <span className="note-preview-text">&nbsp; {props.note.text}</span>
        </div>
    );
}