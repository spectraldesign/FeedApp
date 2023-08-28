import { voteOnPoll } from '@/common/API/poll'
import { errorMessage, successMessage } from '@/common/hooks'
import { Poll } from '@/common/interfaces/poll'
import { Badge, Button, Card, Center, Flex, Group, Text } from '@mantine/core'
import { notifications } from '@mantine/notifications'
import { useRouter } from 'next/router'

interface PollComponentProps {
  poll: Poll
}


const PollComponent = ({ poll }: PollComponentProps) => {
  const router = useRouter()
  const registerVote = async (poll: Poll, isPositive: boolean) => {
    if (poll.isClosed) {
      return notifications.show(errorMessage('Cannot vote on closed poll!'))
    }
    const res = await voteOnPoll(poll.id, isPositive)
    if (res == 201) {
      notifications.show(successMessage('Vote registered!'))
      router.push(`/poll/result/${poll.id}`);
    } else {
      notifications.show(errorMessage('Voting failed!'))
    }
  }
  return (
    <Center>
      <Card miw={'150px'} w={'500px'} shadow='sm' padding='lg' radius='md' withBorder>
        <Group position='right'>
          <Badge color={poll.isClosed ? 'pink' : 'green'} variant='light' fz={'sm'}>
            {poll.isClosed ? 'Closed' : 'Open'}
          </Badge>
        </Group>
        <Group position='center' mt='md' mb='xs'>
          <Flex direction={'column'}>
            <Text align='center' weight={500} fz={'m'}>
              Poll Question:
            </Text>
            <Text align='center' weight={500} fz={'xl'}>
              {poll.question}
            </Text>
          </Flex>
        </Group>

        <Text size='sm' color='dimmed'>
          {`Poll by: ${poll.creatorName}`}
        </Text>

        <Button variant='light' color='green' fullWidth mt='md' radius='md' onClick={() => registerVote(poll, true)}>
          YES
        </Button>
        <Button variant='light' color='red' fullWidth mt='md' radius='md' onClick={() => registerVote(poll, false)}>
          NO
        </Button>
      </Card>
    </Center>
  )
  
}

export default PollComponent
