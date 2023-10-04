export default class NotesApi {

    async getNotes() {
        var result;

        await fetch(`https://localhost:7278/api/notes/getnotes`,
            {
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

    async getNote(id) {
        var result;

        await fetch(`https://localhost:7278/api/notes/getnote/${id}`,
            {
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

    async createNote(note) {
        var result;

        await fetch(`https://localhost:7278/api/notes/createnote`,
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                },
                body: JSON.stringify(note)
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

    async updateNote(note) {
        var result;

        await fetch(`https://localhost:7278/api/notes/updatenote`,
            {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: 'Bearer ' + localStorage.getItem('token')
                },
                body: JSON.stringify(note)
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

    async deleteNote(id) {
        var result;

        await fetch(`https://localhost:7278/api/notes/deletenote/${id}`,
            {
                method: 'DELETE',
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
}