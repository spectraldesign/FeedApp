import { notifications } from '@mantine/notifications'
import { useQuery } from '@tanstack/react-query'
import { AxiosError } from 'axios'
import { defaultArgs, errorMessage, successMessage } from '..'
import { getPollById } from '../../API/poll'
import { Poll } from '../../interfaces/poll'

const getUsePollKey = (pollId: string) => ['UsePoll', pollId]

export const usePoll = (pollId: string) => {
  return useQuery<Poll, AxiosError>(getUsePollKey(pollId), () => getPollById(pollId), {
    ...defaultArgs,
    onError: (err: AxiosError) => {
      notifications.show(errorMessage(err.cause?.message))
    },
    onSuccess: () => {
      notifications.show(successMessage())
    },
  })
}
