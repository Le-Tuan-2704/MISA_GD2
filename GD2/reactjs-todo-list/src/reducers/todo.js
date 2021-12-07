const initialState = {
    list: [
        { id: "1", title: "Buy a new gaming laptop" },
        { id: "2", title: "Complete a previous task" },
        { id: "3", title: "Create video for Youtobe" },
        { id: "4", title: "Create a new portfolio site" },
    ],

    activeId: null,
}

const todoReducer = (state = initialState, action) => {
    switch (action.type) {
        case "SET_TODO_LIST": {
            return {
                ...state,
                list: action.payload,
            }
        }

        case "ADD_TODO": {
            // get id max
            let idMax = 0;
            state.list.forEach(todo => {
                if (idMax < Number(todo.id)) {
                    idMax = Number(todo.id);
                }
            });
            // add id for formValue
            const newTodo = action.payload;
            newTodo["id"] = (idMax + 1) + "";
            console.log(newTodo);

            const newTodoList = [...state.list];
            newTodoList.push(newTodo);
            localStorage.setItem("todoList", JSON.stringify(newTodoList));
            return {
                ...state,
                list: newTodoList,
            }
        }

        case "DEL_TODO": {
            const index = state.list.findIndex(x => x.id === action.payload);
            if (index < 0) return;
            const newTodoList = [...state.list];
            newTodoList.splice(index, 1);
            localStorage.setItem("todoList", JSON.stringify(newTodoList));
            return {
                ...state,
                list: newTodoList,
            };
        }

        case "CLEANADD_TODO": {
            localStorage.setItem("todoList", null);
            return {
                ...state,
                list: [],
            };
        }

        default:
            return state;
    }
}

export default todoReducer;