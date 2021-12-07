
export const setTodoList = (list) => {
    return {
        type: "SET_TODO_LIST",
        payload: list,
    }
}

export const addTodo = (todo) => {
    return {
        type: "ADD_TODO",
        payload: todo,
    }
}

export const delTodo = (todo) => {
    return {
        type: "DEL_TODO",
        payload: todo,
    }
}

export const cleanAllTodo = (todo) => {
    return {
        type: "CLEANADD_TODO",
        payload: todo,
    }
}