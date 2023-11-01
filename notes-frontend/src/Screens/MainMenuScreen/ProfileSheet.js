import { useEffect, useRef } from "react";
import { changePassword } from "../../auth/AuthService";
import "./Styles/ProfileSheetStyles.css";

export default function ProfileSheet(props) {

    const aboutMeRef = useRef();

    const maleGenderRadio = useRef();
    const femaleGenderRadio = useRef();
    const otherGenderRadio = useRef();

    useEffect(() => {
        if (props.user.gender === "Male") {
            maleGenderRadio.current.checked = true;
        }
        else if (props.user.gender === "Female") {
            femaleGenderRadio.current.checked = true;
        }
        else if (props.user.gender === "Other") {
            otherGenderRadio.current.checked = true;
        }

    }, [])

    const setUserGender = (gender) => {
        props.setUserGender(gender);
    }

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
                </div>
            </div>
            <div className="profile-options-container">
                <div className="d-flex flex-column align-items-center">
                    <span>О себе</span>
                    <textarea ref={aboutMeRef} defaultValue={props.user.aboutMe} className="profile-about-me-text" placeholder="Write something about yourself" />
                    <button onClick={() => updateAboutMe()}>Сохранить</button>
                </div>
                <div className="d-flex flex-column align-items-center">
                    <span>Картинка профиля</span>
                    <input type="file" accept="image/*" onChange={(e) => setUserImage(e)} />
                </div>
                <div className="d-flex flex-column align-items-center">
                    <span>Гендер</span>
                    <div className="d-flex">
                        <input className="ms-4 me-1" ref={maleGenderRadio} type="radio" name="gender" 
                        onChange={() => setUserGender('Male')} />Мужской
                        <input className="ms-4 me-1" ref={femaleGenderRadio} type="radio" name="gender"
                        onChange={() => setUserGender('Female')} />Женский
                        <input className="ms-4 me-1" ref={otherGenderRadio} type="radio" name="gender" 
                        onChange={() => setUserGender('Other')} />Другой
                    </div>
                </div>
                <div className="d-flex flex-column align-items-center">
                    <span>Пароль</span>
                    <button onClick={() => changePassword()}>Изменить пароль</button>
                </div>
                <div className="d-flex flex-column align-items-center">
                    <span>Аккаунт</span>
                    <button className="profile-delete-button" onClick={() => deleteUser()}>Удалить аккаунт</button>
                </div>
            </div>
            <span>Дата регистрации: {props.user.registrationDate}</span>
        </div>
    );
}