export default class UserApi {

    async getUserInfo() {
        var result;

        await fetch(/*`usersapi/user/getuser`*/ `http://localhost:5124/api/user/getuser`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        })
            .then(response => {
                return response.json();
            })
            .then((requestResult) => {
                result = requestResult;
            },
                (error) => {
                    console.error(error);
                });

        return result;
    }

    async deleteUser() {
        await fetch(/*`usersapi/user/deleteuser`*/ `http://localhost:5124/api/user/deleteuser`, {
            method: 'DELETE',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        });
    }

    async setUserImage(image) {

        const formData = new FormData();
        formData.append('image', image);

        await fetch(/*`usersapi/user/setuserimage`*/ `http://localhost:5124/api/user/setuserimage`, {
            method: 'PUT',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            },
            body: formData
        });
    }

    async getPrimaryUserInfo() {
        var result;

        await fetch(/*`usersapi/user/getprimaryuserinfo`*/ `http://localhost:5124/api/user/getprimaryuserinfo`, {
            method: 'GET',
            headers: {
                Accept: 'text/plain',
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        })
            .then(response => {
                return response.json();
            })
            .then((requestResult) => {
                result = requestResult;
            },
                (error) => {
                    console.log(error);
                });

        return result;
    }

    async updateAboutMe(aboutMeText) {

        await fetch(/*`usersapi/user/updateaboutme?`*/ `http://localhost:5124/api/user/updateaboutme?` + new URLSearchParams({
            aboutMeText: aboutMeText
        }),
            {
                method: 'PUT',
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                }
            });
    }

    async setUserGender(gender) {

        await fetch(/*`usersapi/user/setusergender?`*/ `http://localhost:5124/api/user/setusergender?` + new URLSearchParams({
            gender: gender
        }),
            {
                method: 'PUT',
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                }
            }
        )
    }
}