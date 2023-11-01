import { useEffect, useState } from "react";
import "./Styles/BackgroundImagesMenuStyles.css";

export default function BackgroundImagesMenu() {

    const [state, setState] = useState({
        backgroundImages: null
    });

    useEffect(() => {
        setState({
            backgroundImages: require('./BackgroundImages').BackgroundImages
        });
    }, []);

    const setBackgroundImage = (image) => {
        localStorage.setItem('bgImg', image);
    }

    return (
        <div className="background-images-menu" onClick={(e) => e.stopPropagation()}>
            <span>Выбор фона</span>
            <div className="background-images">
                {Array.isArray(state.backgroundImages) && state.backgroundImages.map(image => (
                    <button className="background-image-button">
                        <img className="background-image" src={image} key={image} onClick={() => setBackgroundImage(image)} />    
                    </button>
                ))}
            </div>
        </div>
    );
}