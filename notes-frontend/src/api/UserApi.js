export default class UserApi {

    async getUserInfo() {
        var result;

        await fetch(`https://localhost:7003/user/getuser`, {
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
        await fetch(`https://localhost:7003/user/deleteuser`, {
            method: 'DELETE',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        });
    }

    async setUserImage(image) {

        const formData = new FormData();
        formData.append('image', image);

        await fetch(`https://localhost:7003/user/setuserimage`, {
            method: 'PUT',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            },
            body: formData
        });
    }

    async getUserImage() {
        var result;

        await fetch(`https://localhost:7003/user/getuserimage`, {
            method: 'GET',
            headers: {
                Accept: 'text/plain',
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        })
            .then(response => {
                return response.text();
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

        await fetch(`https://localhost:7003/user/updateaboutme?` + new URLSearchParams({
            aboutMeText: aboutMeText
        }),
            {
                method: 'PUT',
                headers: {
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                }
            });
    }
}