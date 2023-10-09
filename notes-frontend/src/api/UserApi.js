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
}