import { api } from '.'
import { Poll } from '../interfaces/poll'

export const getAllPolls = async () => {
  return (await api.get<Poll[]>(`/api/poll`)).data
}

export const getPollById = async (pollId: string) => {
  return (await api.get<Poll>(`/api/poll/${pollId}`)).data
}

export const voteOnPoll = async (pollId: string, isPositive:boolean) => {
  return (await api.post(`/api/vote/${pollId}`, {"isPositive": isPositive}).catch(err=>{}))?.status
}
