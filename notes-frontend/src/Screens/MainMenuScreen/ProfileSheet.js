import "./Styles/ProfileSheetStyles.css";

export default function ProfileSheet(props) {

    const setUserImage = (e) => {
        console.log(e.target.files[0]);
        props.setUserImage(e.target.files[0]);
    }

    return (
        <div className="profile-sheet" onClick={(e) => e.stopPropagation()}>
            <div className="profile-preview-container">
                <img src={props.user.userImageLink} className="profile-icon"/>
                <div>
                    <span>{props.user.username}</span> <br />
                    <span>{props.user.email}</span> <br />
                    <span>Gender: {props.user.gender}</span>
                </div>
            </div>
            <div className="profile-options-container">
                <div className="d-flex flex-column align-items-center">
                    <span>About me</span>
                    <textarea className="profile-about-me-text" placeholder="Write something about yourself" />
                    <button>Save</button>
                </div>
                <div className="d-flex flex-column align-items-center">
                    <span>Profile image</span>
                    <input type="file" accept="image/*" onChange={(e) => setUserImage(e)} />
                </div>
                <div className="d-flex flex-column align-items-center">
                    <span>Password</span>
                    <button>Change password</button>
                </div>
                <div className="d-flex flex-column align-items-center">
                    <span>Account</span>
                    <button className="profile-delete-button">Delete account</button>
                </div>
            </div>
            <span>Registration date: {props.user.registrationDate}</span>
        </div>
    );
}