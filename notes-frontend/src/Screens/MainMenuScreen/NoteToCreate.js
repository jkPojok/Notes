import "../MainMenuScreen/Styles/NoteToCreateStyles.css";

export default function PreviewNote(props) {

    const createBlankNote = () => {
        props.createBlankNote();
    }

    return (
        <div className="create-note-preview" onClick={() => createBlankNote()}>
            <span className="create-note-preview-title">Создать заметку</span>
            <span className="create-note-preview-text">Нажмите сюда чтобы создать заметку</span>
        </div>
    );
}