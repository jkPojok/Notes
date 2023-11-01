export default class NotesApi {

    async getNotes() {
        var result;

        await fetch(/*`notesapi/notes/getnotes`*/ `http://localhost:5009/api/notes/getnotes`,
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

        await fetch(/*`notesapi/notes/getnote/${id}`*/ `http://localhost:5009/api/notes/getnote/${id}`,
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

        await fetch(/*`notesapi/notes/createnote`*/ `http://localhost:5009/api/notes/createnote`,
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

        await fetch(/*`notesapi/notes/updatenote`*/ `http://localhost:5009/api/notes/updatenote`,
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

        await fetch(/*`notesapi/notes/deletenote/${id}`*/ `http://localhost:5009/api/notes/deletenote/${id}`,
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