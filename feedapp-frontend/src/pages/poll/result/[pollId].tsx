import { usePoll } from '@/common/hooks/polls/usePoll'
import PollResultComponent from '@/components/pollResults'
import { useRouter } from 'next/router'

const PollResultPage = () => {
  const router = useRouter()
  const { pollId } = router.query
  const safePollId = typeof pollId == 'string' ? pollId : '' //This looks pretty bad
  const { data: poll } = usePoll(safePollId)
  if (!poll) return <></>

  return <PollResultComponent poll={poll} />
}

export default PollResultPage
