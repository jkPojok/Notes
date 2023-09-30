import "./Styles/NoteStyles.css"

export default function Note(props) {

    return (
      <div className="note-container">
        <div className="note">
            <span>{props.note.title}</span> <br />
            <span>{props.note.text}</span> <br />
            <span>{props.note.dateOfCreation}</span> <br />
            <span>{props.note.dateOfEditing}</span> <br />
        </div>
      </div>  
    );
}