import { useRef } from "react";
import { changePassword } from "../../auth/AuthService";
import "./Styles/ProfileSheetStyles.css";

export default function ProfileSheet(props) {

    const aboutMeRef = useRef();

    const setUserImage = (e) => {
        props.setUserImage(e.target.files[0]);
    }

    const updateAboutMe = () => {
        props.updateAboutMe(aboutMeRef.current.value);
    }

    const deleteUser = () => {
        props.deleteUser();
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
                    <textarea ref={aboutMeRef} defaultValue={props.user.aboutMe} className="profile-about-me-text" placeholder="Write something about yourself" />
                    <button onClick={() => updateAboutMe()}>Save</button>
                </div>
                <div className="d-flex flex-column align-items-center">
                    <span>Profile image</span>
                    <input type="file" accept="image/*" onChange={(e) => setUserImage(e)} />
                </div>
                <div className="d-flex flex-column align-items-center">
                    <span>Password</span>
                    <button onClick={() => changePassword()}>Change password</button>
                </div>
                <div className="d-flex flex-column align-items-center">
                    <span>Account</span>
                    <button className="profile-delete-button" onClick={() => deleteUser()} >Delete account</button>
                </div>
            </div>
            <span>Registration date: {props.user.registrationDate}</span>
        </div>
    );
}