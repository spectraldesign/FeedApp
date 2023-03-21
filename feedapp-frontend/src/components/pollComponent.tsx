import { Poll } from '@/common/interfaces/poll'
import { Badge, Button, Card, Center, Flex, Group, Text } from '@mantine/core'

interface PollComponentProps {
  poll: Poll
}

const PollComponent = ({ poll }: PollComponentProps) => {
  return (
    <Center>
      <Card miw={'150px'} w={'500px'} shadow='sm' padding='lg' radius='md' withBorder>
        <Group position='right'>
          <Badge color={poll.isClosed ? 'pink' : 'green'} variant='light' fz={'md'}>
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

        <Button variant='light' color='green' fullWidth mt='md' radius='md'>
          YES
        </Button>
        <Button variant='light' color='red' fullWidth mt='md' radius='md'>
          NO
        </Button>
      </Card>
    </Center>
  )
}

export default PollComponent
