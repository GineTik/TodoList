import { post } from "./RequestSender.js";

export default class TaskService {
    createTask() {
        return post("/Tasks/CreateTask");
    }
    removeTask(id) {
        return post("/Tasks/RemoveTask", { id: id });
    }
    updateTask({ id, completed, text, expirationTime }) {
        return post("/Tasks/UpdateTask", { id, completed, text, expirationTime });
    }
    updateOrderTask(sortedIds) {
        return post("/Tasks/UpdateTaskOrder", { sortedIds: sortedIds })
    }
}