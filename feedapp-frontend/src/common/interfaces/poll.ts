export interface Poll {
  id: string
  question: string
  isPrivate: true
  isClosed: true
  endTime: Date
  creatorId: string
  creatorName: string
  countVotes: number
  positiveVotes: number
  negativeVotes: number
}
