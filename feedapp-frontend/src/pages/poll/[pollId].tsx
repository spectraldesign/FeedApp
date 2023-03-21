import { usePoll } from '@/common/hooks/polls/usePoll'
import PollComponent from '@/components/pollComponent'
import { useRouter } from 'next/router'

const PollPage = () => {
  const router = useRouter()
  const { pollId } = router.query
  const safePollId = typeof pollId == 'string' ? pollId : '' //This looks pretty bad
  const { data: poll } = usePoll(safePollId)
  if (!poll) return <></>

  return <PollComponent poll={poll} />
}

export default PollPage
